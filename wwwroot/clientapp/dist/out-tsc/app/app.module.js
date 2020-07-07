import { __decorate } from "tslib";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ListaFuncionario } from './Funcionario/ListaFuncionario.component';
import { DataService } from './Shared/DataService';
import { NgxPaginationModule } from 'ngx-pagination';
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            ListaFuncionario
        ],
        imports: [
            BrowserModule,
            NgxPaginationModule,
            HttpClientModule
        ],
        providers: [
            DataService
        ],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map