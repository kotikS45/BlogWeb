import http_common from "../../http_common";
import { IPostItem } from "../../interfaces/news";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import PostItemCardShort from "../news/PostItemCardShort";
import { ITagItem } from "../../interfaces/tag";
import TagItemCard from "./TagItemCard";

const TagItem: React.FC = () => {
  const [category, setCategory] = useState<ITagItem>();
  const [posts, setPosts] = useState<IPostItem[]>([]);

  const { urlSlug } = useParams();

  useEffect(() => {
    http_common.get<ITagItem>(`api/tag/urlSlug/${urlSlug}`)
      .then(resp => {
        const { data } = resp;
        setCategory(data);
      })
      .catch(error => console.log(error));
      
    http_common.get<IPostItem[]>(`api/post/tag/${urlSlug}`)
    .then(resp => {
      const { data } = resp;
      setPosts(data);
    })
    .catch(error => console.log(error));
  }, []);

  return (
    <div className="container">
      <div className="tagContainer">
        {category && (
          <TagItemCard {...category}/>
        )}
      </div>
      <div className="postsContainer">
        {posts.map(x => (<PostItemCardShort {...x}/>))}
      </div>
    </div>
  );
}

export default TagItem;