interface Post {
  id: number;
  threadId: number;
  replyTime: string;
  message: string;
  likeCount: number;
  user: User;
}
