import { Cliente } from "./cliente";

export class Factura {
  constructor(public id:number = 0, public numero:number = 0, public fecha:string = "", public vence:string = "", public modoPago:string = "", public condicionPago:string = "", public usaComprobante:boolean = false, public ncf: string = "", public estado: string = "", public clienteId:number = 0, public companiaId:number = 0, public cliente:Cliente = new Cliente){}
}
