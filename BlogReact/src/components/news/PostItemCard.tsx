import { IPostItem } from '../../interfaces/news';
import { formatDate } from '../../services/date';
import { Link } from 'react-router-dom';
import './style.css';

const PostItemCard: React.FC<IPostItem> = (props) => {
  const { title, description, postedOn, category, tags, urlSlug } = props;

  const descriptionParagraphs = description.split(/\r?\n/);

  return (
    <div className='NewContainer'>
      <Link to={`/news/post/${urlSlug}`}>
        <header>
          <div style={{width: '80%'}}>
            <h2 style={{margin: '10px 0px 10px 15px'}}>{title}</h2>
          </div>
          <div style={{width: '20%', display: 'flex', justifyContent: 'end', alignItems: 'center'}}>
            <span style={{ textAlign: 'right', paddingRight: '10px'}}>{formatDate(postedOn)}</span>
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
        <div style={{
          width: "50%",
          textAlign: "right"
        }}>
          {tags.map(tag => (
            <Link to={`/tags/${tag.urlSlug}`} key={tag.id}>
              <span className='tag'>{tag.name}</span>
            </Link>
          ))}
        </div>
      </footer>
    </div>
  );
}

export default PostItemCard;