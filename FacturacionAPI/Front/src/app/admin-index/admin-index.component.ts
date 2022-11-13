import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { Empresa } from '../models/empresa';
import { APIService } from '../services/api.service';

@Component({
  selector: 'app-admin-index',
  templateUrl: './admin-index.component.html',
  styleUrls: ['./admin-index.component.css']
})
export class AdminIndexComponent implements OnInit {

  empresas: Empresa[] = [];

  constructor(private _APIService: APIService) { }

  ngOnInit(): void {
    this._APIService.BuscarEmpresas().subscribe(e=>{
      this.empresas=e;
    })
  }

  Delete(id: number){
    this._APIService.BorrarEmpresa(id).subscribe((ok)=>{
      if (ok.ok){
        this._APIService.BuscarEmpresas().subscribe(e=>{
          this.empresas=e;
        })
      } else {
        Swal.fire(
          "Alerta",
          "La Empresa no se elimino porque tiene documentos asociados",
          "warning"
        )
      }
    })
  }
}
