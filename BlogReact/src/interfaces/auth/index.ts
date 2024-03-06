export interface IUserLoginInfo {
  username: string,
  image: string
}

export interface IUserTockenInfo {
  username: string,
  email: string,
  roles: string
}

export interface ILogin {
  userNameOrEmail: string,
  password: string
}

export interface ILoginResult {
  token: string
}

export interface IRegister {
  username: string,
  email: string,
  password: string
}

export interface IAvatarFile {
  file: File
}