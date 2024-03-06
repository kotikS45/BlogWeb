import http_common from "../../http_common";
import { useEffect, useState } from "react";
import { IUserTockenInfo } from "../../interfaces/auth";
import { Link, useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import { Button, Card, Form, Input, Modal, message } from "antd";
import { ITagCreate, ITagItem } from "../../interfaces/tag";

const TagList: React.FC = () => {
  const [list, setList] = useState<ITagItem[]>([]);
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const navigator = useNavigate();

  useEffect(() => {
    http_common.get<ITagItem[]>("/api/Tag")
      .then((resp: { data: ITagItem[] }) => {
        const { data } = resp;
        setList(data);
      })
      .catch((error: Error) => console.log(error));
  }, []);

  const tags = list.map(x => (
    <Link key={x.id} to={`/tags/${x.urlSlug}`}>
      <p>{x.name}</p>
    </Link>
  ));

  const isAdmin = () => {
    if (localStorage.token){
      const user: IUserTockenInfo = jwtDecode<IUserTockenInfo>(localStorage.token);
      if (user) {
        const roles = user.roles.split(', ').map(role => role.trim());
        return roles.includes('admin');
      }
    }
    return false;
  }
  
  const success = (message: string) => {
    messageApi.open({
      type: 'success',
      duration: 10,
      content: `Category ${message} created`,
    });
  };
  
  const onClear = () => {
    form.resetFields();
  }

  const error = (message: string) => {
    messageApi.open({
        type: 'error',
        duration: 10,
        content: message,
    });
  };
  
  const createCategory = async (tag: ITagCreate) => {
    try {
      await http_common.post("/api/tag", tag, {
        headers: {
            "Content-Type": "multipart/form-data"
        }
      })
      .then(() => {
        success(tag.name);
      })
      .finally(() => {
        onClear();
        navigator("/");
      });
    } catch (e) {
      error((e as Error).message);
    }
  }

  const tagForm = (
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
        label="Name"
        name="name"
        rules={[
          {required: true, message: 'This field is required'},
          {min: 2, message: 'Field must have 2 characters minimum'}
        ]}>
        <Input />
      </Form.Item>
      <Form.Item
        label="Url Slug"
        name="urlSlug"
        rules={[
          {required: true, message: 'This field is required',},
          {min: 2, message: 'Field must have 2 characters minimum',},
        ]}
        hasFeedback
      >
        <Input />
      </Form.Item>
      <Form.Item
        label="Description"
        name="description"
        rules={[
          {required: true, message: 'This field is required',},
          {min: 2, message: 'Field must have 2 characters minimum',},
        ]}
        hasFeedback
      >
        <Input.TextArea />
      </Form.Item>
    </Form>
    </>
  )
  
  const handleOk = () => {
    form
    .validateFields()
    .then(values => {
      createCategory(values)
      setIsModalOpen(false)
    })
    .catch(errorInfo => {
      console.log('Validation failed:', errorInfo);
    })
  }

  const handleCancel = () => {
    setIsModalOpen(false)
  }

  return (
    <div style={{ marginTop: '22px', marginLeft: '5%', width: '150px' }}>
      <Card title="Tags" bordered={false} >
        {isAdmin() && <Button style={{width: '100px', marginBottom: '10px', color: 'blue'}} onClick={() => setIsModalOpen(true)}>+</Button>}
        {tags}
      </Card>
      <Modal title="Create Tag" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
        {contextHolder}
        {tagForm}
      </Modal>
    </div>
  );
}

export default TagList;