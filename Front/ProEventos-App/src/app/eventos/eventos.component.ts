import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})

export class EventosComponent implements OnInit {


  constructor(private http: HttpClient) { }



  public eventos : any = [];
  public eventosFiltrados : any = [];
  widthImg:number = 150;
  marginImg:number =2;
  showImg:boolean = true;
  private _filtroLista:string = '';

  public get filtroLista()
  {
    return this._filtroLista;
  }

  public set filtroLista(value:string){
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista?this.filtrarEventos(this._filtroLista):this.eventos;
  }

  filtrarEventos(_filtrarPor: string):any {
    _filtrarPor = _filtrarPor.toLocaleLowerCase();

     return this.eventos.filter(
        (evento:any) => evento.tema.toLocaleLowerCase().indexOf(_filtrarPor) !== -1 ||
                        evento.local.toLocaleLowerCase().indexOf(_filtrarPor) !== -1
      )
  }

  ngOnInit() {
    this.getEventos();
  }

  alterarImagem(){
    this.showImg = !this.showImg;
  }

  public getEventos():void{
    this.http.get(`https://localhost:6001/api/eventos`).subscribe(
      response =>
      {
        this.eventos = response,
        this.eventosFiltrados = response
      },
      error => console.error(error)
    )
  }

}
