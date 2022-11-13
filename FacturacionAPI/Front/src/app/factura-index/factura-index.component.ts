import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Factura } from '../models/factura';
import { APIService } from '../services/api.service';
import { modoPago } from "../models/modoPago";
import { condicionPago } from "../models/condicionPago";
import { estatus } from "../models/estatusFactura";
import { FacturaDetalle } from '../models/factura-detalle';
import { Empresa } from '../models/empresa';
import { Producto } from '../models/producto';
import Swal from 'sweetalert2';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { Cliente } from '../models/cliente';

@Component({
  selector: 'app-factura-index',
  templateUrl: './factura-index.component.html',
  styleUrls: ['./factura-index.component.css']
})
export class FacturaIndexComponent implements OnInit {

  facturaPagar = new Factura();
  cliente!: Cliente;
  facturas: Factura[]=[];
  detallesPagar:FacturaDetalle[] = [];
  productos: Producto[] = [];
  modoPago = modoPago;
  condicionPago = condicionPago;
  estatus = estatus;
  Math = Math;
  totalPagar: number = 0;
  devuelta: number = 0;
  pagoConValue:number = 0;
  currentDate = `${new Date().getDate()}/${new Date().getMonth()}/${new Date().getFullYear()}`;
  @ViewChild("pagoCon") pagoCon!: ElementRef;
  @ViewChild("closeBtn") closeBtn!: ElementRef;
  @ViewChild("factura") factura!: ElementRef;

  constructor(public _APIService: APIService) { }

  ngOnInit(): void {
    this._APIService.BuscarFacturas().subscribe(f=>{
      this.facturas = f;
    })
    this._APIService.BuscarProductos().subscribe(p=>{
      this.productos = p;
    })
  }

  setFactura(id: number){
    if (id!==-1){
      this._APIService.BuscarFactura(id).subscribe(f=>{
        this.facturaPagar = f;
        this._APIService.BuscarDetalles(id).subscribe(d=>{
          this.detallesPagar = d;
          this.totalPagar += this.getSubtotal()+this.getItbis();
        })
      });
    } else {
      this.facturaPagar = new Factura();
      this.totalPagar = 0;
      this.devuelta = 0;
      this.pagoCon.nativeElement.value="";
      this.cliente = new Cliente();
    }
  }

  Delete(id: number){
    this._APIService.BorrarFactura(id);
    setTimeout(() => {
      this._APIService.BuscarFacturas().subscribe(f=>{
        this.facturas = f;
      })
    }, 500);
  }

  pagoConInput(){
    let pagoValor = parseFloat(this.pagoCon.nativeElement.value) || 0;
    this.pagoConValue = pagoValor;
    if (pagoValor>this.totalPagar ){
      this.devuelta = Math.round((pagoValor - this.totalPagar) * 100) / 100;
    } else {
      this.devuelta = 0;
    }
  }

  getEmpresa(){
    let empresa = localStorage.getItem("empresa");
    if (empresa != null){
      return JSON.parse(empresa) as Empresa;
    }
    return new Empresa();
  }

  getProducto(id: number){
    return this.productos.filter(p => p.id==id)[0];
  }

  getSubtotal(){
    let sum = 0;
    this.detallesPagar.forEach(d=>{
      sum += this.getProducto(d.productoId).precio*d.cantidad;
    })
    return sum;
  }

  getItbis(){
    let sum = 0;
    let empresa = this.getEmpresa();
    this.detallesPagar.forEach(d=>{
      sum += this.getProducto(d.productoId).precio*d.cantidad*empresa.itbis/100;
    })
    return Math.round(sum * 100) / 100;
  }

  Pagar(){
    let pagoCon = parseFloat(this.pagoCon.nativeElement.value) || 0;
    if (this.totalPagar>pagoCon){
      Swal.fire(
        "Alerta",
        "La cantidad con la que va a pagar es menor al total de la factura",
        "warning"
      )
    } else {
      this.facturaPagar.estado = estatus.PAGADA;
      this._APIService.PagarFactura(this.facturaPagar, this.detallesPagar);
      this.factura.nativeElement.classList.remove("d-none");
      html2canvas(this.factura.nativeElement, { scale: 4 }).then((canvas) => {
        const imageGeneratedFromTemplate = canvas.toDataURL('image/png');
        const fileWidth = 200;
        const generatedImageHeight = (canvas.height * fileWidth) / canvas.width;
        let PDF = new jsPDF('p', 'mm', 'a4',);
        PDF.addImage(imageGeneratedFromTemplate, 'PNG', 0, 5, fileWidth, generatedImageHeight,);
        PDF.html(this.factura.nativeElement.innerHTML)
        PDF.save('Factura.pdf');
      });
      this.factura.nativeElement.classList.add("d-none");
      this.closeBtn.nativeElement.click();
      this.setFactura(-1);
      setTimeout(() => {
        this._APIService.BuscarFacturas().subscribe(f=>{
          this.facturas = f;
        })
      }, 1000);
    }
  }
}
