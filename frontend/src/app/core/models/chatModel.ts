export interface ChatModel{
    id: number;
    name: string;
    chatTypeId: number;
    lastMessageText: string;
    lastMessageCreatedAt: Date;
}