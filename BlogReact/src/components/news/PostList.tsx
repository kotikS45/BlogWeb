import http_common from "../../http_common";
import { IPostItem } from "../../interfaces/news";
import { useEffect, useState } from "react";
import PostItemCard from "./PostItemCard";

const PostList: React.FC = () => {
  const [list, setList] = useState<IPostItem[]>([]);

  useEffect(() => {
    http_common.get<IPostItem[]>("/api/Post")
      .then((resp: { data: IPostItem[]; }) => {
        const { data } = resp;
        setList(data);
      })
      .catch((error: Error) => console.log(error));
  }, []);

const content = list.map(x => <PostItemCard key={x.id} {...x}/>);

  return (
    <div style={{display: 'flex', justifyContent: 'center', flexDirection: 'column', width: '50%', marginLeft: '20%'}}>
      {content}
    </div>
  );
}

export default PostList;