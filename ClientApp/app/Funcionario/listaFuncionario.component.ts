import {Component, OnInit} from "@angular/core";
import { DataService } from '../Shared/DataService';
import {Funcionario} from '../Shared/funcionario';
@Component({
    selector: "lista-funcionario",
    templateUrl: "listaFuncionario.component.html",
    styleUrls: []
})
export class ListaFuncionario implements OnInit{
    constructor(private data: DataService){
        
    }

    public funcionarios: Funcionario[] = [];
        ngOnInit(): void  {
           this.data.carregarFuncionarios()
           .subscribe(sucess => {
               if(sucess){
                   this.funcionarios = this.data.funcionarios;
               }
           });
        }
}