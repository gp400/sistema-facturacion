import { Component, OnInit } from '@angular/core';
import { Cliente } from '../models/cliente';
import { APIService } from '../services/api.service';

@Component({
  selector: 'app-clientes-index',
  templateUrl: './clientes-index.component.html',
  styleUrls: ['./clientes-index.component.css']
})
export class ClientesIndexComponent implements OnInit {

  clientes: Cliente[]=[];

  constructor(private _APIService: APIService) { }

  ngOnInit(): void {
    this._APIService.BuscarClientes().subscribe(c=>{
      this.clientes = c;
    })
  }

  Delete(id: number){
    this._APIService.BorrarCliente(id);
    setTimeout(() => {
      this._APIService.BuscarClientes().subscribe(c=>{
        this.clientes = c;
      })
    }, 500);
  }

}
