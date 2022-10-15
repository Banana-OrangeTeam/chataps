import { Component, OnInit } from '@angular/core';
import { ChatModel } from 'src/app/core/models/chatModel';
import { ChatService } from 'src/app/core/services/chat.service';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.css']
})
export class ChatListComponent implements OnInit {

  userChatList: ChatModel[]; 

  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.chatService.GetChatList().subscribe((data)=>{ 
      this.userChatList = data;
    });
  }
}
