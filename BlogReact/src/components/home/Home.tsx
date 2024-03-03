import http_common from "../../http_common";
import { IPostItem } from "../../interfaces/news";
import { useEffect, useState } from "react";
import PostItemCardShort from "../news/PostItemCardShort";
import './style.css';

const Home: React.FC = () => {
  const [list, setList] = useState<IPostItem[]>([]);

  useEffect(() => {
    http_common.get<IPostItem[]>("/api/Post")
      .then(resp => {
        const { data } = resp;
        setList(data);
      })
      .catch(error => console.log(error));
  }, []);

  const content = list.map(x => (
    <PostItemCardShort key={x.id} {...x}/>
  ));

  return (
    <div className="list">
      {content}
    </div>
  );
}

export default Home;