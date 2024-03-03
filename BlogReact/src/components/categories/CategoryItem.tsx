import http_common from "../../http_common";
import { ICategoryItem } from "../../interfaces/category";
import { IPostItem } from "../../interfaces/news";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import PostItemCardShort from "../news/PostItemCardShort";
import CategoryItemCard from "./CategoryItemCard";

const CategoryItem: React.FC = () => {
  const [category, setCategory] = useState<ICategoryItem>();
  const [posts, setPosts] = useState<IPostItem[]>([]);

  const { urlSlug } = useParams();

  useEffect(() => {
    http_common.get<ICategoryItem>(`api/category/urlSlug/${urlSlug}`)
      .then(resp => {
        const { data } = resp;
        setCategory(data);
      })
      .catch(error => console.log(error));
      
    http_common.get<IPostItem[]>(`api/post/category/${urlSlug}`)
    .then(resp => {
      const { data } = resp;
      setPosts(data);
    })
    .catch(error => console.log(error));
  }, []);

  return (
    <div className="container">
      <div className="categoryContainer">
        {category && (
          <CategoryItemCard {...category}/>
        )}
      </div>
      <div className="postsContainer">
        {posts.map(x => (<PostItemCardShort {...x}/>))}
      </div>
    </div>
  );
}

export default CategoryItem;