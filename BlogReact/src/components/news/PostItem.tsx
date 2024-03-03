import http_common from "../../http_common";
import { IPostItem } from "../../interfaces/news";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import PostItemCard from "./PostItemCard";

const PostItem: React.FC = () => {
  const [post, setPost] = useState<IPostItem>();

  const { urlSlug } = useParams();

  useEffect(() => {      
    http_common.get<IPostItem>(`api/post/urlSlug/${urlSlug}`)
    .then(resp => {
      const { data } = resp;
      setPost(data);
    })
    .catch(error => console.log(error));
  }, []);

  return (
    <div className="PostItemContainer">
      {post && (
      <PostItemCard {...post}/>
      )}
    </div>
  );
}

export default PostItem;