import { __decorate } from "tslib";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
let DataService = class DataService {
    constructor(http) {
        this.http = http;
        this.funcionarios = [];
    }
    carregarFuncionarios() {
        return this.http.get("/api/funcionarios")
            .pipe(map((data) => {
            this.funcionarios = data;
            return true;
        }));
    }
};
DataService = __decorate([
    Injectable()
], DataService);
export { DataService };
//# sourceMappingURL=DataService.js.map