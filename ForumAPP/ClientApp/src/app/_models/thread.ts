interface Thread {
  id: number;
  title: string;
  content: string;
  seenCount: number;
  lastReply: string;
  user: User;
  lastReplyUser: User;
  postCount: number;
  tagId: number;
}
