import http_common from "../../http_common";
import { ICategoryItem } from "../../interfaces/category";
import { useEffect, useState } from "react";
import CategoryItemCard from "./CategoryItemCard";

const CategoryList: React.FC = () => {
  const [list, setList] = useState<ICategoryItem[]>([]);

  useEffect(() => {
    http_common.get<ICategoryItem[]>("/api/Category")
      .then(resp => {
        const { data } = resp;
        setList(data);
      })
      .catch(error => console.log(error));
  }, []);

  const content = list.map(x => (
    <CategoryItemCard {...x}/>
  ));

  return (
    <div className="container">
      {content}
    </div>
  );
}

export default CategoryList;