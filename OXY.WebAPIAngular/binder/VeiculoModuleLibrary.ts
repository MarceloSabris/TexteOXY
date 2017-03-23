import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { VeiculoComponente } from './VeiculoComponente';
import { FormsModule } from '@angular/forms';
@NgModule(
    {
      
        imports: [BrowserModule, FormsModule],
        declarations: [VeiculoComponente],
        bootstrap: [VeiculoComponente]
    })
export class VeiculoModuleLibrary {

}