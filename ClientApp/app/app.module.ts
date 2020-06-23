import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {ListaFuncionario} from './Funcionario/ListaFuncionario.component';

@NgModule({
  declarations: [
    AppComponent,
    ListaFuncionario
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
