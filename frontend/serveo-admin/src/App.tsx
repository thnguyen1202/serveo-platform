import { RouterProvider } from 'react-router-dom';

//import Test from './components/test';
import { router } from './routes';

function App() {
  return <RouterProvider router={router} />;

  // function App() {
  //   return (
  //     <div
  //       className="
  //       flex
  //       h-screen
  //       items-center
  //       justify-center
  //       bg-slate-100
  //       text-3xl
  //       font-bold
  //     "
  //     >
  //       Serveo Admin
  //       <Test />
  //     </div>
  //   );
}

export default App;
