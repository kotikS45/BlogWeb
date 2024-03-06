import { Button, Modal, Image, Upload, UploadFile, UploadProps } from 'antd';
import { UploadOutlined } from '@ant-design/icons';
import { APP_ENV } from '../../env';
import { IAvatarFile, IUserLoginInfo } from '../../interfaces/auth';
import { useState } from 'react';
import { RcFile } from 'antd/es/upload';
import http_common from '../../http_common';

const ProfileTab: React.FC<IUserLoginInfo> = (props) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [previewImage, setPreviewImage] = useState<string>('');
  const [file, setFile] = useState<UploadFile | null>();
  let { username, image } = props;
  
  if (image) image = `${APP_ENV.BASE_URL}/images/${image}`;
  else image = `${APP_ENV.BASE_URL}/images/avatar.png`;

  const avatarStyle = {
    backgroundImage: `url(${image})`,
    width: '40px',
    height: '40px',
    borderRadius: '50%',
    backgroundSize: 'cover',
    margin: '15px 15px 10px 0px'
  };

  const containerStyle = {
    display: 'flex',
    alighItems: 'center',
    justifyContent: 'space-between',
    fontSize: '20px'
  }

  const updateAvatar = () => {
    setIsModalOpen(true)
    setPreviewImage(image);
  }
  
  const handleOk = () => {
    const avatar = file?.originFileObj;
    http_common.post<IAvatarFile>(`/api/account/avatar`, avatar)
      .then(resp => {
        console.log(resp);
      })
    setIsModalOpen(false)
  }
  
  const handleCancel = () => {
    setIsModalOpen(false)
  }
  
  const handlerChange: UploadProps['onChange'] = ({fileList: newFile}) => {
    const newFileList = newFile.slice(-1);
    setFile(newFileList[0]);
    handlerPreview(newFileList[0]);
  }
  
  const handlerPreview = async (file: UploadFile) => {
    if (!file.url && !file.preview){
        file.preview = URL.createObjectURL(file.originFileObj as RcFile);
    }
    setPreviewImage(file.url || (file.preview as string));
  }

  return (
    <div style={containerStyle}>
      <div style={avatarStyle} onClick={updateAvatar}/>
      <span>{username}</span>
      <Modal title="Update avatar" open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
        <Image style={{borderRadius: 10}} height={100} src={previewImage || 'https://lightwidget.com/wp-content/uploads/localhost-file-not-found.jpg'}/>
        <Upload
          beforeUpload={() => false}
          maxCount={1}
          listType="picture-card"
          onChange={handlerChange}
          onPreview={handlerPreview}
          fileList={file ? [file] : []}
          accept="image/*">
          <Button icon={<UploadOutlined/>}>Select</Button>
        </Upload>
      </Modal>
    </div>
  );
  
}

export default ProfileTab;

