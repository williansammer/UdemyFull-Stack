import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  constructor(private http: HttpClient) { }

  public eventos : any;

  ngOnInit() {
    this.getEventos();
  }

  public getEventos():void{
    this.http.get(`https://localhost:6001/api/eventos`).subscribe(
      response => this.eventos = response,
      error => console.error(error)
    )
  }

}
