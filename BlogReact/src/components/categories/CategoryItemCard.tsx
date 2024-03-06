import './style.css';
import { ICategoryEdit, ICategoryItem } from '../../interfaces/category';
import { Link, useNavigate } from 'react-router-dom';
import { Button, Form, Input, Modal, message } from 'antd';
import { IUserTockenInfo } from '../../interfaces/auth';
import { jwtDecode } from 'jwt-decode';
import { useEffect, useState } from 'react';
import http_common from '../../http_common';

const CategoryItemCard: React.FC<ICategoryItem> = (props) => {
  const { id, name, description, urlSlug } = props;
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const navigator = useNavigate();
  
  const descriptionParagraphs = description.split(/\r?\n/);

  useEffect(() => {
    form.setFieldsValue(props);
  }, [])

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
      content: `Category ${message} updated`,
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
  
  const updateCategory = async (category: ICategoryEdit) => {
    category.id = id;
    try {
      await http_common.put("/api/category", category, {
        headers: {
            "Content-Type":"multipart/form-data"
        }
      }).finally(() => {
        success(category.name);
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
        <Input autoComplete={"name"}/>
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
        <Input autoComplete={"urlSlug"}/>
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
        <Input.TextArea autoComplete={"description"}/>
      </Form.Item>
    </Form>
    </>
  )
  
  const handleOk = () => {
    form
    .validateFields()
    .then(values => {
      updateCategory(values)
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
    <div className='containerShort' style={{width: '100%'}}>
      <Link to={`/categories/news/${urlSlug}`}>
        <header>
          <div>
            <h2 style={{margin: '10px 15px 10px 15px'}}>{name}</h2>
          </div>
          {isAdmin() && <Button style={{width: '60px', margin: '10px 15px 10px 15px', color: 'blue'}} onClick={() => setIsModalOpen(true)}>Edit</Button>}
        </header>
      </Link>
      <div style={{
        margin: '0',
        padding: '5px 0px 5px 0px',
        boxShadow: "inset -2px 0px 10px #001529, inset 2px 0px 10px #001529, inset 0px -2px 10px #001529"
      }}>
        {descriptionParagraphs.map((paragraph, index) => (
          <p key={index} className='paragraph'>{paragraph}</p>
        ))}
      </div>
      <Modal title="Edit category" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
        {contextHolder}
        {categoryForm}
      </Modal>
    </div>
  );
}

export default CategoryItemCard;