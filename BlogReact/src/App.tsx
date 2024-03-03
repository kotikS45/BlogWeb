import './App.css'
import {Route, Routes} from "react-router-dom";
import Home from './components/home/Home';
import ContainerDefault from './components/containers/ContainerDefault';
import CategoryList from './components/categories/CategoryList';
import PostList from './components/news/PostList';
import CategoryItem from './components/categories/CategoryItem';
import TagItem from './components/tags/TagItem';
import PostItem from './components/news/PostItem';

const App: React.FC = () => {
  return (
    <Routes>
      <Route path="/" element={<ContainerDefault />} >
        <Route index element={<Home />} />
        <Route path="categories" >
          <Route index element={<CategoryList />} />
          <Route path='news/:urlSlug' element={<CategoryItem />} />
        </Route>
        <Route path='tags/:urlSlug' element={<TagItem/>}/>
        <Route path="news" >
          <Route index element={<PostList />} />
          <Route path='post/:urlSlug' element={<PostItem />} />
        </Route>
      </Route>
    </Routes>
  )
}

export default App
