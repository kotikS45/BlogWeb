import {IUserTockenInfo} from "../../interfaces/auth";

export enum AuthReducerActionType {
  LOGIN_USER = "AUTH_LOGIN_USER"
}

export interface IAuthReducerState {
  isAuth: boolean,
  user: IUserTockenInfo | null
}

const initState: IAuthReducerState = {
  isAuth: false,
  user: null
}

export const AuthReducer = (state = initState, action: any): IAuthReducerState => {
  switch(action.type){
    case AuthReducerActionType.LOGIN_USER: {
      return {
        isAuth: true,
        user: action.payload
      };
    }
    default:
      return state;
  }
}