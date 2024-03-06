import { combineReducers, configureStore } from '@reduxjs/toolkit'
import { AuthReducer } from '../components/auth/AuthReducer'

export const rootReducer = combineReducers({
  auth: AuthReducer
});

export const store = configureStore({
  devTools: true,
  reducer: rootReducer,
})

export type AppStore = typeof store
export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch