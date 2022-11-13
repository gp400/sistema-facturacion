import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { Cliente } from '../models/cliente';
import { APIService } from '../services/api.service';

@Component({
  selector: 'app-cliente-form',
  templateUrl: './cliente-form.component.html',
  styleUrls: ['./cliente-form.component.css']
})
export class ClienteFormComponent implements OnInit {

  cliente:Cliente = new Cliente();
  @ViewChild("form") form!: NgForm;

  constructor(private _APIService: APIService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id =  this.route.snapshot.paramMap.get("id");
    if (id){
      this._APIService.BuscarCliente(parseInt(id)).subscribe(c=>{
        this.cliente = c;
      })
    }
  }

  Submit(){
    if (this.form.valid){
      if (this.cliente.id == 0){
        this._APIService.InsertarCliente(this.cliente).subscribe((ok: { ok: any; })=>{
          if (ok.ok){
            setTimeout(() => {
              this.router.navigate(["/"])
            }, 1000);
          } else {
            Swal.fire(
              "Alerta",
              "Existe un cliente con esa cedula",
              "warning"
            )
          }
        });
      } else {
        this._APIService.ActualizarCliente(this.cliente).subscribe((ok: { ok: any; })=>{
          if (ok.ok){
            setTimeout(() => {
              this.router.navigate(["/"])
            }, 1000);
          } else {
            Swal.fire(
              "Alerta",
              "Existe un cliente con esa cedula",
              "warning"
            )
          }
        })
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
