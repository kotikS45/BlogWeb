import React from 'react';
import { Breadcrumb, Layout, Menu, theme, Card } from 'antd';
import { Link, Outlet } from 'react-router-dom';
import { ITagItem } from '../../interfaces/tag';
import { useState, useEffect } from 'react';
import http_common from "../../http_common";

const { Header, Content, Footer } = Layout;

const ContainerDefault: React.FC = () => {
  const [list, setList] = useState<ITagItem[]>([]);

  useEffect(() => {
    http_common.get<ITagItem[]>("/api/Tag")
      .then(resp => {
        const { data } = resp;
        setList(data);
      })
      .catch(error => console.log(error));
  }, []);

  const {
    //token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const tags = list.map(x => (
    <Link key={x.id} to={`/tags/${x.urlSlug}`}>
      <p>{x.name}</p>
    </Link>
  ));

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header style={{ display: 'flex', alignItems: 'center', padding: '0'}}>
        <div className="demo-logo" />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={['1']}
          items={[
            {
              key: 1,
              label: <Link to="/">Home</Link>
            },
            {
              key: 2,
              label: <Link to="/categories">Categories</Link>
            },
            {
              key: 3,
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
          <Card title="Tags" bordered={false} style={{ marginTop: '22px', marginLeft: '5%', width: '10%' }}>
            {tags}
          </Card>
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