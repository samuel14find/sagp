import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http'

import { AppComponent } from './app.component';
import {ListaFuncionario} from './Funcionario/ListaFuncionario.component';
import {DataService} from './Shared/DataService'

@NgModule({
  declarations: [
    AppComponent,
    ListaFuncionario
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
