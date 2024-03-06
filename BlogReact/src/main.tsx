import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import { BrowserRouter } from 'react-router-dom'
import { Provider } from 'react-redux'
import { store } from './app/store.ts';
import { setAuthToken } from './helpers/setAuthToken.ts'
import { IUserTockenInfo } from './interfaces/auth/index.ts'
import { jwtDecode } from 'jwt-decode'
import { AuthReducerActionType } from './components/auth/AuthReducer.ts'

if (localStorage.token) {
  setAuthToken(localStorage.token)
  const user: IUserTockenInfo = jwtDecode<IUserTockenInfo>(localStorage.token);
  store.dispatch({
    type: AuthReducerActionType.LOGIN_USER,
    payload: {
      username: user.username,
      email: user.email,
      roles: user.roles
    } as IUserTockenInfo
  });
}

ReactDOM.createRoot(document.getElementById('root')!).render(
  <Provider store={store}>
    <BrowserRouter>
      <App/>
    </BrowserRouter>
  </Provider>
)
