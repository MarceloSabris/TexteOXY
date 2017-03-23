import { Component } from "@angular/core";
import { Veiculo } from "../Model/Veiculo";
@Component(
    {
        selector: "veiculo-UI",
        templateUrl :"Veiculo.html"
    }
)
export class VeiculoComponente
{
    veiculoobj: Veiculo = new Veiculo();
}