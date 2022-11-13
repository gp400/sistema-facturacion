import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { Producto } from '../models/producto';
import { APIService } from '../services/api.service';

@Component({
  selector: 'app-producto-form',
  templateUrl: './producto-form.component.html',
  styleUrls: ['./producto-form.component.css']
})
export class ProductoFormComponent implements OnInit {

  producto:Producto = new Producto();
  @ViewChild("form") form!: NgForm;

  constructor(private _APIService: APIService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id =  this.route.snapshot.paramMap.get("id");
    if (id){
      this._APIService.BuscarProducto(parseInt(id)).subscribe(c=>{
        this.producto = c;
      })
    }
  }

  Submit(){
    if (this.form.valid){
      if (this.producto.id == 0){
        this._APIService.InsertarProducto(this.producto).subscribe((ok: { ok: any; })=>{
          if (ok.ok){
            setTimeout(() => {
              this.router.navigate(["/productos"])
            }, 1000);
          } else {
            Swal.fire(
              "Alerta",
              "Existe un producto con ese codigo",
              "warning"
            )
          }
        })
      } else {
        this._APIService.ActualizarProducto(this.producto).subscribe((ok: { ok: any; })=>{
          if (ok.ok){
            setTimeout(() => {
              this.router.navigate(["/productos"])
            }, 1000);
          } else {
            Swal.fire(
              "Alerta",
              "Existe un producto con ese codigo",
              "warning"
            )
          }
        });
      }
    } else {
      Swal.fire(
        "Alerta",
        "Llene todos los campos",
        "warning"
      )
    }
  }

}
