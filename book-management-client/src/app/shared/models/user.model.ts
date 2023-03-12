export class UserTokenModel {
  authToken!: string;
  isAuthenticated!: boolean;
}

export class User {
  userName!: string;
  userPassword!: string;
  userEmail!: string;
  userContact!: string;
  role: any;
  isActive!: boolean;
  createdBy!: string;
  createdDateTime!: Date;
  updatedBy!: string;
  updatedDateTime!: Date;
}
