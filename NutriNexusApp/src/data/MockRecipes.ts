import { Recipe } from '../models/Recipe';

export const MOCK_RECIPES = [
  new Recipe({
    id: 1,
    name: 'Pasta Primavera',
    description:
      'really good pasta. amazing actually. the best I have ever had and that any one will ever have.',
    imageUrl: '/delicious-pasta-dish.jpg',
    timeEstimate: '30min',
    servingSize: '4',
    rating: 4.3,
  }),
  new Recipe({
    id: 2,
    name: 'Mac and Cheese',
    description: 'really good pasta',
    imageUrl: '/mac-and-cheese.jpg',
    timeEstimate: '30min',
    servingSize: '4',
  }),
  new Recipe({
    id: 3,
    name: 'Cornish Hen',
    description: 'really good pasta',
    imageUrl: '/cornish-hen.jpg',
    timeEstimate: '30min',
    servingSize: '4',
  }),
  new Recipe({
    id: 4,
    name: 'Soy Garlic Chicken',
    description: 'really good pasta',
    imageUrl: '/soy-garlic-chicken.jpg',
    timeEstimate: '30min',
    servingSize: '4',
  }),
  new Recipe({
    id: 5,
    name: 'Blueberry Scones',
    description: 'really good pasta',
    imageUrl: '/blueberry-scones.jpg',
    timeEstimate: '30min',
    servingSize: '4',
  }),
];
