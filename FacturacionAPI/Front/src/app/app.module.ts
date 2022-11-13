import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ClientesIndexComponent } from './clientes-index/clientes-index.component';
import { AuthGuard } from './guards/auth.guard';
import { ClienteFormComponent } from './cliente-form/cliente-form.component';
import { ProductosIndexComponent } from './productos-index/productos-index.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ProductoFormComponent } from './producto-form/producto-form.component';
import { FacturaIndexComponent } from './factura-index/factura-index.component';
import { FacturaFormComponent } from './factura-form/factura-form.component';
import { AdminIndexComponent } from './admin-index/admin-index.component';
import { AdminFormComponent } from './admin-form/admin-form.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "", component: ClientesIndexComponent, canActivate: [AuthGuard] },
  { path: "admin", component: AdminIndexComponent, canActivate: [AuthGuard] },
  { path: "adminForm", component: AdminFormComponent, canActivate: [AuthGuard] },
  { path: "adminForm/:id", component: AdminFormComponent, canActivate: [AuthGuard] },
  { path: "clienteForm", component: ClienteFormComponent, canActivate: [AuthGuard] },
  { path: "clienteForm/:id", component: ClienteFormComponent, canActivate: [AuthGuard] },
  { path: "productos", component: ProductosIndexComponent, canActivate: [AuthGuard] },
  { path: "productoForm", component: ProductoFormComponent, canActivate: [AuthGuard] },
  { path: "productoForm/:id", component: ProductoFormComponent, canActivate: [AuthGuard] },
  { path: "facturas", component: FacturaIndexComponent, canActivate: [AuthGuard] },
  { path: "facturaForm", component: FacturaFormComponent, canActivate: [AuthGuard] },
  { path: "facturaForm/:id", component: FacturaFormComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ClientesIndexComponent,
    ClienteFormComponent,
    ProductosIndexComponent,
    NavbarComponent,
    ProductoFormComponent,
    FacturaIndexComponent,
    FacturaFormComponent,
    AdminIndexComponent,
    AdminFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
