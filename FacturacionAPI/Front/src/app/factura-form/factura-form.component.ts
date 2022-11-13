import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Factura } from '../models/factura';
import { FacturaDetalle } from '../models/factura-detalle';
import { modoPago } from "../models/modoPago";
import { condicionPago } from "../models/condicionPago";
import { estatus } from "../models/estatusFactura";
import { Cliente } from '../models/cliente';
import { APIService } from '../services/api.service';
import { Empresa } from '../models/empresa';
import { ActivatedRoute, Router } from '@angular/router';
import { Producto } from '../models/producto';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-factura-form',
  templateUrl: './factura-form.component.html',
  styleUrls: ['./factura-form.component.css']
})
export class FacturaFormComponent implements OnInit {

  factura:Factura = new Factura();
  detalles:FacturaDetalle[] = [];
  clientes:Cliente[] = [];
  productos:Producto[] = [];
  modoPago = modoPago;
  condicionPago = condicionPago;
  estatus = estatus;
  Math = Math;
  @ViewChild("productoDetalle") productoDetalle!:ElementRef;
  @ViewChild("cantidadDetalle") cantidadDetalle!:ElementRef;
  @ViewChild("form") form!: NgForm;

  constructor(private _APIService: APIService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this._APIService.BuscarClientes().subscribe(c=>{
      this.clientes = c;
    })
    this._APIService.BuscarProductos().subscribe(p=>{
      this.productos = p;
    })
    let id =  this.route.snapshot.paramMap.get("id");
    if(id){
      this._APIService.BuscarFactura(parseInt(id)).subscribe(f => {
        f.fecha = f.fecha.slice(0, 10);
        f.vence = f.vence.slice(0, 10);
        this.factura = f;
        this._APIService.BuscarDetalles(f.id).subscribe(d=>{
          this.detalles=d;
        })
      })
    }
  }

  setNCF(): void{
    if(this.factura.usaComprobante){
      this._APIService.BuscarFacturas().subscribe(f => {
        let prevNCF = "";
        let empresa = localStorage.getItem("empresa");
        f.sort((a, b)=> {
          return parseInt(a.ncf.slice(3)) - parseInt(b.ncf.slice(3))
        })
        let conNCF = f.filter(fac=>fac.ncf!="");
        if (empresa != null){
          if(conNCF.length==0){
            prevNCF = (JSON.parse(empresa) as Empresa).ncf;
          } else {
            prevNCF = conNCF[conNCF.length-1].ncf;
          }
          let ncf = parseInt(prevNCF.slice(3))+1;
          let ncfString = ncf.toString();
          while (ncfString.length < 8){
            ncfString = "0"+ncfString;
          }
          this.factura.ncf = prevNCF.slice(0,3)+ncfString;
        }
      })
    } else {
      this.factura.ncf="";
    }
  }

  agregarDetalle(){
    let cantidad = parseInt(this.cantidadDetalle.nativeElement.value) || -1;
    let productoId = parseInt(this.productoDetalle.nativeElement.value) || -1;
    if (cantidad <= 0){
      Swal.fire(
        "Error",
        "La cantidad debe ser mayor a 0",
        "error"
      )
    } else if (productoId <= 0){
      Swal.fire(
        "Alerta",
        "Debe seleccionar un producto",
        "warning"
      )
    } else {
      this.detalles.push(new FacturaDetalle(0, 0, productoId, cantidad))
      this.cantidadDetalle.nativeElement.value = 0;
    }
  }

  getProducto(id: number){
    return this.productos.filter(p => p.id==id)[0];
  }

  getEmpresa(){
    let empresa = localStorage.getItem("empresa");
    if (empresa != null){
      return JSON.parse(empresa) as Empresa;
    }
    return new Empresa();
  }

  getSubtotal(){
    let sum = 0;
    this.detalles.forEach(d=>{
      sum += this.getProducto(d.productoId).precio*d.cantidad;
    })
    return this.Math.round(sum * 100)/100;
  }

  getItbis(){
    let sum = 0;
    let empresa = this.getEmpresa();
    this.detalles.forEach(d=>{
      sum += this.getProducto(d.productoId).precio*d.cantidad*empresa.itbis/100;
    })
    return Math.round(sum * 100) / 100;
  }

  borrarDetalle(id: number){
    this.detalles = this.detalles.filter(d=>d.productoId!=id);
  }

  validarDetalle(id: number){
    return this.detalles.filter(d=>d.productoId==id).length==0;
  }

  public Submit(){
    if (this.form.valid){
      if(this.detalles.length !== 0){
        if (this.factura.id == 0){
          this._APIService.InsertarFactura(this.factura, this.detalles).subscribe((ok: string[])=>{
            if (ok.length == 0){
              setTimeout(() => {
                this.router.navigate(["/facturas"])
              }, 1000);
            } else if (ok[0] == "-1") {
              Swal.fire(
                "Alerta",
                "Existe una factura con ese numero",
                "warning"
              )
            } else if (ok[0]!= "-1"){
              Swal.fire(
                "Alerta",
                "La cantidad en stock de al menos 1 de sus productos es menor a la cantidad seleccionada",
                "warning"
              )
            }
          });
        } else {
          this._APIService.ActualizarFactura(this.factura, this.detalles).subscribe((ok: string[])=>{
            if (ok.length == 0){
              setTimeout(() => {
                this.router.navigate(["/facturas"])
              }, 1000);
            } else if (ok[0] == "-1") {
              Swal.fire(
                "Alerta",
                "Existe una factura con ese numero",
                "warning"
              )
            } else if (ok[0]!= "-1"){
              Swal.fire(
                "Alerta",
                "La cantidad en stock de al menos 1 de sus productos es menor a la cantidad seleccionada",
                "warning"
              )
            }
          });
        }
      } else {
        Swal.fire(
          "Alerta",
          "Debe seleccionar al menos 1 producto",
          "warning"
        )
      }
    } else {
      Swal.fire(
        "Alerta",
        "Asegurese todos los campos",
        "warning"
      )
    }
  }
}
