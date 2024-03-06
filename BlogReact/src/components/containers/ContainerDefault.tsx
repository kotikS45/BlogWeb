import React from 'react';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import { Link, Outlet } from 'react-router-dom';
import { useState, useEffect } from 'react';
import http_common from "../../http_common";
import ProfileTab from '../profile/PofileTab';
import { MenuItemType } from "antd/es/menu/hooks/useItems";
import { useAppSelector } from '../../app/hooks';
import { IUserLoginInfo } from '../../interfaces/auth';
import { Button } from 'antd';
import {useNavigate} from "react-router-dom";
import Authorize from '../auth/authorize';
import TagList from '../tags/TagList';

const { Header, Content, Footer } = Layout;

const ContainerDefault: React.FC = () => {
  const [user, setUser] = useState<IUserLoginInfo>();
  const navigator = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    http_common.get<IUserLoginInfo>("/api/account")
      .then((resp: { data: IUserLoginInfo }) => {
        setUser(resp.data);
      });
  }, []);

  const showModal = () => {
    setIsModalOpen(true)
  };
  
  const SignOut = () => {
    localStorage.removeItem('token')
    setUser(undefined)
    navigator("/")
    window.location.reload()
  }

  const auth = useAppSelector((store: { auth: any; }) => store.auth);

  let items: MenuItemType[] = [];
  if (auth.isAuth){
    items.push({
      key: 1,
      label: <Button onClick={SignOut}>Sign Out</Button>
    },
    {
      key: 2,
      label: user ? <ProfileTab {...user} /> : null
    })
  }
  else {
    items.push({
      key: 2,
      label: <Button type="primary" onClick={showModal}>Sign In</Button>
    });
  }

  const {
    //token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header style={{ display: 'flex', alignItems: 'center', padding: '0'}}>
        <div className="demo-logo" />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={['1']}
          items={[
            ...items,
            {
              key: 3,
              label: <Link to="/">Home</Link>
            },
            {
              key: 4,
              label: <Link to="/categories">Categories</Link>
            },
            {
              key: 5,
              label: <Link to="/news">News</Link>
            }
            ]}
          style={{ flex: 1, minWidth: 0 }}
        />
      </Header>
      <Content style={{ padding: '0 48px' }}>
        <Breadcrumb style={{ margin: '16px 0' }}>
          <Breadcrumb.Item>Home</Breadcrumb.Item>
          <Breadcrumb.Item>List</Breadcrumb.Item>
        <Breadcrumb.Item>App</Breadcrumb.Item>
        </Breadcrumb>
        <Authorize isModalOpen={isModalOpen} setIsModalOpen={setIsModalOpen} setUser={setUser}/>
        <div
          style={{
            //background: colorBgContainer,
            minHeight: 280,
            padding: 24,
            //borderRadius: borderRadiusLG,
            display: 'flex',
            flexDirection: 'row'
          }}
        >
          <TagList/>
          <div style={{ width: "90%"}}>
            <Outlet />
          </div>
        </div>
      </Content>
      <Footer style={{ textAlign: 'center' }}>
        Ant Design ©{new Date().getFullYear()} Created by Ant UED
      </Footer>
    </Layout>
  );
};

export default ContainerDefault;