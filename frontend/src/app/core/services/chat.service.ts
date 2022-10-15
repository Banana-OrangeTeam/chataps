import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { getChatsUrl } from 'src/app/configs/api-endpoints';
import { ChatModel } from '../models/chatModel';
import { AuthenticationService } from './authentificationService';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private httpOption = {
    headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.getToken()
    })
  };

  constructor(private http: HttpClient, private authService: AuthenticationService) { }
  
  private getToken(): any{
    return localStorage.getItem('token')?.toString();
  }

  public GetChatList():Observable<ChatModel[]> {
    return this.http.get<ChatModel[]>(getChatsUrl, this.httpOption);
  }
}
