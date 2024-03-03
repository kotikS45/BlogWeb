import { IPostItem } from '../../interfaces/news';
import { formatDate } from '../../services/date';
import { Link } from 'react-router-dom';
import './style.css';

const PostItemCardShort: React.FC<IPostItem> = (props) => {
  const { title, shortDescription, postedOn, category, urlSlug } = props;

  const descriptionParagraphs = shortDescription.split(/\r?\n/);

  return (
    <div className='containerShort'>
      <Link to={`/news/post/${urlSlug}`}>
        <header>
          <div>
            <h2 style={{margin: '10px 15px 10px 15px'}}>{title}</h2>
          </div>
        </header>
      </Link>
      <div style={{
        margin: '0',
        padding: '5px 0px 5px 0px',
        boxShadow: "inset -2px 0px 10px #001529, inset 2px 0px 10px #001529"
      }}>
        {descriptionParagraphs.map((paragraph, index) => (
          <p key={index} className='paragraph'>{paragraph}</p>
        ))}
      </div>
      <footer>
        <div style={{
          width: "50%"
        }}>
          <Link to={`/categories/news/${category.urlSlug}`}>
            <span className='category'>{category.name}</span>
          </Link>
        </div>
        <div className='date'>
          <span>{formatDate(postedOn)}</span>
        </div>
      </footer>
    </div>
  );
}

export default PostItemCardShort;