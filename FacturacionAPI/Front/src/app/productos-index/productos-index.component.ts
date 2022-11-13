import { Component, OnInit } from '@angular/core';
import { Producto } from '../models/producto';
import { APIService } from '../services/api.service';

@Component({
  selector: 'app-productos-index',
  templateUrl: './productos-index.component.html',
  styleUrls: ['./productos-index.component.css']
})
export class ProductosIndexComponent implements OnInit {

  productos: Producto[] = [];

  constructor(private _APIService: APIService) { }

  ngOnInit(): void {
    this._APIService.BuscarProductos().subscribe(c=>{
      this.productos = c;
    })
  }

  Delete(id: number){
    this._APIService.BorrarProducto(id);
    setTimeout(() => {
      this._APIService.BuscarProductos().subscribe(c=>{
        this.productos = c;
      })
    }, 500);
  }

}
