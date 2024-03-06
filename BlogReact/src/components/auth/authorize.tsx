import { Form, Input, Modal, message } from "antd";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { ILogin, ILoginResult, IRegister, IUserLoginInfo } from "../../interfaces/auth";
import { setAuthToken } from "../../helpers/setAuthToken";
import { useNavigate } from "react-router";
import http_common from "../../http_common";
import { AuthReducerActionType } from "./AuthReducer";
import React from "react";

interface AuthorizeProps {
  isModalOpen: boolean;
  setIsModalOpen: React.Dispatch<React.SetStateAction<boolean>>;
  setUser: React.Dispatch<React.SetStateAction<IUserLoginInfo | undefined>>;
}

const Authorize: React.FC<AuthorizeProps> = ({ isModalOpen, setIsModalOpen, setUser }) => {
  const [pageRegister, setPageRegister] = useState<boolean>(false);
  const [form] = Form.useForm();
  const dispatch = useDispatch();
  const navigator = useNavigate();
  const [messageApi, contextHolder] = message.useMessage();

  const register = async (userRegister: IRegister) => {
    try {
      const resp = await http_common.post<ILoginResult>("/api/account/register", userRegister);
      const { token } = resp.data;
      localStorage.token = token;
      setAuthToken(localStorage.token);
  
      const info = await http_common.get<IUserLoginInfo>("api/account");
      const user = info.data;
  
      if (user) {
        dispatch({
          type: AuthReducerActionType.LOGIN_USER,
          payload: {
            username: user.username,
            image: user.image
          } as IUserLoginInfo
        });
        setUser(user);
      }
  
      success(user.username);
      onClear();
      navigator("/");
    } catch (e) {
      error((e as Error).message);
    }
  }

  const signin = async (userLogin: ILogin) => {
    try {
      const resp = await http_common.post<ILoginResult>("/api/account/login", userLogin);
      const { token } = resp.data;
      localStorage.token = token;
      setAuthToken(localStorage.token);
  
      const info = await http_common.get<IUserLoginInfo>("api/account");
      const user = info.data;
  
      if (user) {
        dispatch({
          type: AuthReducerActionType.LOGIN_USER,
          payload: {
            username: user.username,
            image: user.image
          } as IUserLoginInfo
        });
        setUser(user);
      }
  
      success(user.username);
      onClear();
      navigator("/");
    } catch (e) {
      error((e as Error).message);
    }
  };

  const success = (message: string) => {
    messageApi.open({
      type: 'success',
      duration: 10,
      content: `Hello ${message}`,
    });
  };

  const error = (message: string) => {
    messageApi.open({
        type: 'error',
        duration: 10,
        content: message,
    });
  };

  const onClear = () => {
    form.resetFields();
  }

  const handleOk = () => {
    if (!pageRegister){
      form
      .validateFields()
      .then(values => {
        signin(values)
        setIsModalOpen(false)
      })
      .catch(errorInfo => {
        console.log('Validation failed:', errorInfo);
      })
    }
    else{
      form
      .validateFields()
      .then(values => {
        register(values)
        setIsModalOpen(false)
      })
      .catch(errorInfo => {
        console.log('Validation failed:', errorInfo);
      })
    }
  }

  const handleCancel = () => {
    setIsModalOpen(false)
  }

  const registerLoginPage = (
    <a style={{}} onClick={(e) => {e.preventDefault(); setPageRegister(!pageRegister) }}>{pageRegister ? "Log In" : "Registration"}</a>
  )

  const loginForm = (
    <>
    <Form
      form={form}
      layout="vertical"
      style={{
        minWidth: '100%',
        display: 'flex',
        flexDirection: "column",
        justifyContent: "center",
        padding: 20
      }}>
      <Form.Item
        label="Username or email"
        name="userNameOrEmail"
        rules={[{
          message: 'Invalid username or email'},
          {required: true, message: 'This field is required'},
          {min: 2, message: 'Field must have 2 characters minimum'}
        ]}>
        <Input />
      </Form.Item>
      <Form.Item
        label="Password"
        name="password"
        rules={[
          {required: true, message: 'This field is required',},
          {min: 4, message: 'Field must have 4 characters minimum',},
        ]}
        hasFeedback
      >
        <Input.Password/>
      </Form.Item>
      {registerLoginPage}
    </Form>
    </>
  )

  const registerForm = (
    <>
    <Form
      form={form}
      layout="vertical"
      style={{
        minWidth: '100%',
        display: 'flex',
        flexDirection: "column",
        justifyContent: "center",
        padding: 20
      }}>
      <Form.Item
        label="Username"
        name="username"
        rules={[{
          message: 'Invalid username'},
          {required: true, message: 'This field is required'},
          {min: 2, message: 'Field must have 2 characters minimum'}
        ]}>
        <Input />
      </Form.Item>
      <Form.Item
        label="Email"
        name="email"
        rules={[{
          message: 'Invalid email'},
          {required: true, message: 'This field is required'},
          {min: 5, message: 'Field must have 5 characters minimum'}
        ]}>
        <Input />
      </Form.Item>
      <Form.Item
        label="Password"
        name="password"
        rules={[
          {required: true, message: 'This field is required',},
          {min: 4, message: 'Field must have 4 characters minimum',},
        ]}
        hasFeedback
      >
        <Input.Password/>
      </Form.Item>
      {registerLoginPage}
    </Form>
    </>
  )

  return (
    <Modal title={pageRegister ? "Registration" : "Sign In"} open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
      {contextHolder}
      {pageRegister ? registerForm : loginForm}
    </Modal>
  );
}

export default Authorize;