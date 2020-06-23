import {Component} from "@angular/core";
@Component({
    selector: "lista-funcionario",
    templateUrl: "listaFuncionario.component.html",
    styleUrls: []
})
export class ListaFuncionario{
    public funcionarios = 
    [
        {
            name: "Samuel S Bicalho Pereira",
            matricula: "1827333",
            id: 1,
            setor: "Gestão de Pessoas"
        },
        {
            name: "Antônio Evaristo Pereira",
            matricula: "297444",
            id: 2,
            setor: "Fazenda"
        },
        {
            name: "Margarida Bicalho Salvador Pereira",
            matricula: "2579852",
            id: 3,
            setor: "Gestão de Compras"
        }
    ]; 
}