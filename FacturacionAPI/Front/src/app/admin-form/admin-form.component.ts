import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { Empresa } from '../models/empresa';
import { APIService } from '../services/api.service';

@Component({
  selector: 'app-admin-form',
  templateUrl: './admin-form.component.html',
  styleUrls: ['./admin-form.component.css']
})
export class AdminFormComponent implements OnInit {

  empresa = new Empresa();
  @ViewChild("form") form!: NgForm;

  constructor(private _APIService: APIService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id =  this.route.snapshot.paramMap.get("id");
    if (id){
      this._APIService.BuscarEmpresaId(parseInt(id)).subscribe(e=>{
        this.empresa = e;
      })
    }
  }

  Submit(){
    if(this.form.valid){
      if (this.empresa.id == 0){
        this._APIService.InsertarEmpresa(this.empresa).subscribe((ok: { ok: any; })=>{
          if (ok.ok){
            setTimeout(() => {
              this.router.navigate(["/admin"])
            }, 1000);
          } else {
            Swal.fire(
              "Alerta",
              "Existe una empresa con ese codigo",
              "warning"
            )
          }
        });
      } else {
        this._APIService.ActualizarEmpresa(this.empresa).subscribe((ok: { ok: any; })=>{
          if (ok.ok == true){
            setTimeout(() => {
              this.router.navigate(["/admin"])
            }, 1000);
          } else {
            Swal.fire(
              "Alerta",
              "Existe una empresa con ese codigo",
              "warning"
            )
          }
        });
      }
    } else {
      Swal.fire(
        "Alerta",
        "Todos los campos son obligatorios",
        "warning"
      )
    }
  }
}
