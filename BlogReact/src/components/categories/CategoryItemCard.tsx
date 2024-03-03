import './style.css';
import { ICategoryItem } from '../../interfaces/category';
import { Link } from 'react-router-dom';

const CategoryItemCard: React.FC<ICategoryItem> = (props) => {
  const { name, description, urlSlug } = props;

  const descriptionParagraphs = description.split(/\r?\n/);

  return (
    <div className='containerShort' style={{width: '100%'}}>
      <Link to={`/categories/news/${urlSlug}`}>
        <header>
          <div>
            <h2 style={{margin: '10px 15px 10px 15px'}}>{name}</h2>
          </div>
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
    </div>
  );
}

export default CategoryItemCard;