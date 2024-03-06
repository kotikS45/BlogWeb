import http_common from "../../http_common";
import { ICategoryCreate, ICategoryItem } from "../../interfaces/category";
import { useEffect, useState } from "react";
import CategoryItemCard from "./CategoryItemCard";
import { IUserTockenInfo } from "../../interfaces/auth";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode";
import { Button, Form, Input, Modal, message } from "antd";

const CategoryList: React.FC = () => {
  const [list, setList] = useState<ICategoryItem[]>([]);
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const navigator = useNavigate();

  useEffect(() => {
    http_common.get<ICategoryItem[]>("/api/Category")
      .then((resp: { data: ICategoryItem[] }) => {
        const { data } = resp;
        setList(data);
      })
      .catch((error: Error) => console.log(error));
  }, []);

  const content = list.map(x => (
    <CategoryItemCard key={x.id} {...x}/>
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
  
  const createCategory = async (category: ICategoryCreate) => {
    try {
      await http_common.post("/api/category", category, {
        headers: {
            "Content-Type":"multipart/form-data"
        }
      })
      .then(() => {
        success(category.name);
      })
      .finally(() => {
        onClear();
        navigator("/");
      });
    } catch (e) {
      error((e as Error).message);
    }
  }

  const categoryForm = (
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
    <div className="container">
      {isAdmin() && <Button style={{width: '100px', marginBottom: '10px', color: 'blue'}} onClick={() => setIsModalOpen(true)}>Create</Button>}
      {content}
      <Modal title="Create category" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
        {contextHolder}
        {categoryForm}
      </Modal>
    </div>
  );
}

export default CategoryList;