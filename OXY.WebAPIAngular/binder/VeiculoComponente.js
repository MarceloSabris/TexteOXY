"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var Veiculo_1 = require("../Model/Veiculo");
var VeiculoComponente = (function () {
    function VeiculoComponente() {
        this.veiculoobj = new Veiculo_1.Veiculo();
    }
    return VeiculoComponente;
}());
VeiculoComponente = __decorate([
    core_1.Component({
        selector: "veiculo-UI",
        templateUrl: "Veiculo.html"
    })
], VeiculoComponente);
exports.VeiculoComponente = VeiculoComponente;
//# sourceMappingURL=VeiculoComponente.js.map