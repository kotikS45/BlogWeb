import http_common from "../../http_common";
import { ICategoryItem } from "../../interfaces/category";
import { useEffect, useState } from "react";
import { Card } from 'antd';
import { Link } from "react-router-dom";

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
    <Link key={x.id} to={`/categories/news/${x.urlSlug}`}>
      <Card key={x.id} title={x.name} bordered={false} style={{ marginTop: '22px', marginLeft: '20%', width: '50%' }}>
        {x.description}
      </Card>
    </Link>
  ));

  return (
    <div style={{display: 'flex', justifyContent: 'center', flexDirection: 'column'}}>
      {content}
    </div>
  );
}

export default CategoryList;