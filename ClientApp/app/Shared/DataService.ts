import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {map} from "rxjs/operators";
import {Funcionario} from "./funcionario";

@Injectable()
export class DataService {
    constructor(private http: HttpClient){

    }
    public funcionarios: Funcionario[] = []; 
    carregarFuncionarios(): Observable<boolean>{
        return this.http.get("/api/funcionarios")
        .pipe(
            map((data: any[])=>{  
                this.funcionarios = data;
                return true;
             })
        )
    }
}