import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { APIService } from '../services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  codigo: string = "";

  constructor(private _APIService: APIService, private router: Router) { }

  ngOnInit(): void {
    console.log(localStorage.getItem("empresa"))
    localStorage.setItem("empresa", "");
  }

  Submit(): void{
    if (this.codigo==""){
      Swal.fire(
        'Alerta',
        'Ingrese el codigo de la empresa',
        'warning'
      )
    } else {
      if (this.codigo == "admin"){
        localStorage.setItem("empresa", JSON.stringify(this.codigo));
        this.router.navigate(["/admin"]);
        return;
      }
      this._APIService.BuscarEmpresaCodigo(this.codigo).subscribe(e => {
        if(e==null){
          Swal.fire(
            'Alerta',
            'Ingrese el codigo correcto',
            'warning'
          )
          this.codigo="";
        } else {
          localStorage.setItem("empresa", JSON.stringify(e));
          this.router.navigate(["/"]);
        }
      })
    }
  }

}
