import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';
import { Cliente } from '../models/cliente';
import { Empresa } from '../models/empresa';
import { estatus } from '../models/estatusFactura';
import { Factura } from '../models/factura';
import { FacturaDetalle } from '../models/factura-detalle';
import { Producto } from '../models/producto';

@Injectable({
  providedIn: 'root'
})
export class APIService {

  constructor(private http: HttpClient, private router: Router) {}

  // Empresas
  public BuscarEmpresas(): Observable<Empresa[]>{
    return this.http.get<Empresa[]>(`https://localhost:7089/Empresa`);
  }

  public BuscarEmpresaId(id: number): Observable<Empresa>{
    return this.http.get<Empresa>(`https://localhost:7089/Empresa/id/${id}`);
  }

  public BuscarEmpresaCodigo(codigo:string): Observable<Empresa>{
    return this.http.get<Empresa>(`https://localhost:7089/Empresa/${codigo}`);
  }

  public InsertarEmpresa(empresa: Empresa): any {
    return this.http.post<{ok: boolean}>("https://localhost:7089/Empresa", { Codigo: empresa.codigo, Nombre: empresa.nombre, Direccion: empresa.direccion, Telefono: empresa.telefono, Itbis: empresa.itbis, Rnc: empresa.rnc, Ncf: empresa.ncf });
  }

  public ActualizarEmpresa(empresa: Empresa): any {
    return this.http.put<{ok: boolean}>("https://localhost:7089/Empresa", { Codigo: empresa.codigo, Nombre: empresa.nombre, Direccion: empresa.direccion, Telefono: empresa.telefono, Itbis: empresa.itbis, Rnc: empresa.rnc, Ncf: empresa.ncf, Id: empresa.id });
  }

  public BorrarEmpresa(id: number): Observable<{ok: boolean}>{
    return this.http.delete<{ok: boolean}>(`https://localhost:7089/Empresa/${id}`);
  }

  //Clientes
  public BuscarClientes(): Observable<Cliente[]>{
    let empresaId = this.BuscarEmpresaIdStorage();
    return this.http.get<Cliente[]>(`https://localhost:7089/Clientes/${empresaId}`)
  }

  public BuscarCliente(id: number): Observable<Cliente>{
    let empresaId = this.BuscarEmpresaIdStorage();
    return this.http.get<Cliente>(`https://localhost:7089/Clientes/${empresaId}/${id}`);
  }

  public InsertarCliente(cliente: Cliente): any {
    let empresaId = this.BuscarEmpresaIdStorage();
    if (empresaId != null){
      return this.http.post<{ok: boolean}>("https://localhost:7089/Clientes", {Nombre: cliente.nombre, Apellido: cliente.apellido, Cedula: cliente.cedula, Telefono: cliente.telefono, Correo: cliente.correo, Direccion: cliente.direccion, Rnc: cliente.rnc, CompaniaId: empresaId });
    }
  }

  public ActualizarCliente(cliente: Cliente): any {
    let empresaId = this.BuscarEmpresaIdStorage();
    if (empresaId != null){
      return this.http.put<{ok: boolean}>("https://localhost:7089/Clientes", {Nombre: cliente.nombre, Apellido: cliente.apellido, Cedula: cliente.cedula, Telefono: cliente.telefono, Correo: cliente.correo, Direccion: cliente.direccion, Rnc: cliente.rnc, CompaniaId: empresaId, Id: cliente.id });
    }
  }

  public BorrarCliente(id: number): void{
    let empresaId = this.BuscarEmpresaIdStorage();
    this.BuscarFacturasPorCliente(id).subscribe(c=>{
      if (c.length == 0){
        this.http.delete(`https://localhost:7089/Clientes/${empresaId}/${id}`).subscribe();
      } else {
        Swal.fire(
          "Error",
          "No se puede eliminar el cliente porque tiene facturas asociadas",
          "error"
        )
      }
    })
  }

  //Productos
  public BuscarProductos(): Observable<Producto[]>{
    let empresaId = this.BuscarEmpresaIdStorage();
    return this.http.get<Producto[]>(`https://localhost:7089/Producto/${empresaId}`)
  }

  public BuscarProducto(id: number): Observable<Producto>{
    let empresaId = this.BuscarEmpresaIdStorage();
    return this.http.get<Producto>(`https://localhost:7089/Producto/${empresaId}/${id}`);
  }

  public InsertarProducto(producto: Producto): any {
    let empresaId = this.BuscarEmpresaIdStorage();
    if (empresaId != null){
      return this.http.post("https://localhost:7089/Producto", { Codigo: producto.codigo, Nombre: producto.nombre, Cantidad: producto.cantidad, Precio: producto.precio, CompaniaId: empresaId });
    }
  }

  public ActualizarProducto(producto: Producto): any {
    let empresaId = this.BuscarEmpresaIdStorage();
    if (empresaId != null){
      return this.http.put("https://localhost:7089/Producto", { Codigo: producto.codigo, Nombre: producto.nombre, Cantidad: producto.cantidad, Precio: producto.precio, CompaniaId: empresaId, Id: producto.id });
    }
  }

  public BorrarProducto(id: number): void{
    let empresaId = this.BuscarEmpresaIdStorage();
    this.BuscarDetallePorProducto(id).subscribe(p=>{
      if (p.length == 0){
        this.http.delete(`https://localhost:7089/Producto/${empresaId}/${id}`).subscribe();
      } else {
        Swal.fire(
          "Error",
          "No se puede eliminar el producto porque tiene facturas asociadas",
          "error"
        )
      }
    })
  }

  //Facturas
  public BuscarFacturas(): Observable<Factura[]>{
    let empresaId = this.BuscarEmpresaIdStorage();
    return this.http.get<Factura[]>(`https://localhost:7089/Factura/${empresaId}`)
  }

  public BuscarFactura(id: number): Observable<Factura>{
    let empresaId = this.BuscarEmpresaIdStorage();
    return this.http.get<Factura>(`https://localhost:7089/Factura/${empresaId}/${id}`);
  }

  public BuscarFacturasPorCliente(clienteId: number): Observable<Cliente[]>{
    let empresaId = this.BuscarEmpresaIdStorage();
    return this.http.get<Cliente[]>(`https://localhost:7089/Factura/clientes/${empresaId}/${clienteId}`)
  }

  public InsertarFactura(factura: Factura, detalle: FacturaDetalle[]): any {
    let empresaId = this.BuscarEmpresaIdStorage();
    if (empresaId != null){
      return this.http.post<Factura>("https://localhost:7089/Factura", { Numero: factura.numero, Fecha: factura.fecha, Vence: factura.vence, ModoPago: factura.modoPago, CondicionPago: factura.condicionPago, UsaComprobante: factura.usaComprobante, Ncf: factura.ncf, Estado: estatus.PENDIENTE, ClienteId: factura.clienteId, CompaniaId: empresaId, FacturaDetalles: detalle });
    }
  }

  public ActualizarFactura(factura: Factura, detalle: FacturaDetalle[]): any {
    let empresaId = this.BuscarEmpresaIdStorage();
    if (empresaId != null){
      return this.http.put("https://localhost:7089/Factura", { Numero: factura.numero, Fecha: factura.fecha, Vence: factura.vence, ModoPago: factura.modoPago, CondicionPago: factura.condicionPago, UsaComprobante: factura.usaComprobante, Ncf: factura.ncf, Estado: factura.estado, ClienteId: factura.clienteId, CompaniaId: empresaId, FacturaDetalles: detalle, Id: factura.id });
    }
  }

  public BorrarFactura(id: number): void{
    let empresaId = this.BuscarEmpresaIdStorage();
    this.http.delete(`https://localhost:7089/Factura/${empresaId}/${id}`).subscribe();
  }

  public PagarFactura(factura: Factura ,detalle: FacturaDetalle[]){
    let empresaId = this.BuscarEmpresaIdStorage();
    if (empresaId != null){
      this.http.put("https://localhost:7089/Factura", { Numero: factura.numero, Fecha: factura.fecha, Vence: factura.vence, ModoPago: factura.modoPago, CondicionPago: factura.condicionPago, UsaComprobante: factura.usaComprobante, Ncf: factura.ncf, Estado: factura.estado, ClienteId: factura.clienteId, CompaniaId: empresaId,  FacturaDetalles: detalle, Id: factura.id }).subscribe()
    }
  }

  //Factura Detalle
  public BuscarDetalles(facturaId: number): Observable<FacturaDetalle[]>{
    return this.http.get<FacturaDetalle[]>(`https://localhost:7089/FacturaDetalle/${facturaId}`)
  }

  public BuscarDetallePorProducto(productoId: number): Observable<Producto[]>{
    return this.http.get<Producto[]>(`https://localhost:7089/FacturaDetalle/productos/${productoId}`)
  }

  //Utils
  BuscarEmpresaIdStorage(): number|null {
    let empresa = localStorage.getItem("empresa");
    if (empresa != null){
      return (JSON.parse(empresa) as Empresa).id;
    }
    return null;
  }

  formatDate(fecha: string): string{
    let date = new Date(fecha);
    return `${date.getDate()}/${date.getMonth()}/${date.getFullYear()}`
  }
}
