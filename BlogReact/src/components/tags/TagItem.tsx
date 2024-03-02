import http_common from "../../http_common";
import { IPostItem } from "../../interfaces/news";
import { useEffect, useState } from "react";
import { Card } from 'antd';
import { useParams } from "react-router-dom";
import { ITagItem } from "../../interfaces/tag";

const TagItem: React.FC = () => {
  const [tag, setTag] = useState<ITagItem>();
  const [posts, setPosts] = useState<IPostItem[]>([]);

  const { urlSlug } = useParams();

  useEffect(() => {
    http_common.get<ITagItem>(`api/tag/urlSlug/${urlSlug}`)
      .then(resp => {
        const { data } = resp;
        setTag(data);
      })
      .catch(error => console.log(error));
      
    http_common.get<IPostItem[]>(`api/post/tag/${urlSlug}`)
    .then(resp => {
      const { data } = resp;
      setPosts(data);
    })
    .catch(error => console.log(error));
  }, [urlSlug]);
  
  const dateToShortString = (date: Date) => {
    if (!date)
        return "";
    
    const options: object = {
        hour: '2-digit',
        minute: '2-digit',
        day: '2-digit',
        month: 'short'
    };

    return date.toLocaleDateString('en-US', options);
  };

  return (
    <div style={{ display: 'flex', justifyContent: 'center', flexDirection: 'column' }}>
      {tag && (
        <Card
          key={tag.id}
          title={tag.name}
          bordered={false}
          style={{ marginTop: '22px', marginLeft: '20%', width: '50%' }}
        >
          {tag.description}
        </Card>
      )}
      {posts.map(x => (
        <Card
          key={x.id}
          title={x.title}
          bordered={false}
          style={{ marginTop: '22px', marginLeft: '20%', width: '50%' }}
        >
          <p>{x.shortDescription}</p>
          <p>{x.description}</p>
          <p>{x.meta}</p>
          <p>{x.urlSlug}</p>
          <p>{x.published}</p>
          <p>{x.postedOn ? dateToShortString(new Date(x.postedOn)) : 'No date available'}</p>
          <p>{x.modified ? dateToShortString(new Date(x.modified)) : 'No date available'}</p>
          <p>{x.category.name}</p>
          <p>{x.tags.map(tag => tag.name).join(" ")}</p>
        </Card>
      ))}
    </div>
  );
}

export default TagItem;