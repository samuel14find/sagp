import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ListaFuncionario = class ListaFuncionario {
    constructor(data) {
        this.data = data;
        this.pag = 1;
        this.contador = 15;
        this.funcionarios = [];
    }
    ngOnInit() {
        this.data.carregarFuncionarios()
            .subscribe(sucess => {
            if (sucess) {
                this.funcionarios = this.data.funcionarios;
            }
        });
    }
};
ListaFuncionario = __decorate([
    Component({
        selector: "lista-funcionario",
        templateUrl: "listaFuncionario.component.html",
        styleUrls: []
    })
], ListaFuncionario);
export { ListaFuncionario };
//# sourceMappingURL=ListaFuncionario.component.js.map