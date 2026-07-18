import { Outlet } from 'react-router-dom';

import Breadcrumb from './components/Breadcrumb';
import Header from './components/Header';
import Sidebar from './components/Sidebar';

export default function AdminLayout() {
  return (
    <div className="flex h-screen bg-background">
      <Sidebar />

      <div className="flex flex-1 flex-col">
        <Header />

        <main className="flex-1 overflow-auto p-6">
          <Breadcrumb />

          <Outlet />
        </main>
      </div>
    </div>
  );
}

// import { Outlet } from 'react-router-dom';

// import Header from './components/Header';
// import Sidebar from './components/Sidebar';

// export default function AdminLayout() {
//   return (
//     <div
//       className="
//             flex
//             min-h-screen
//         "
//     >
//       <Sidebar />

//       <div
//         className="
//                 flex-1
//                 flex
//                 flex-col
//             "
//       >
//         <Header />

//         <main
//           className="
//                     flex-1
//                     p-6
//                     bg-gray-50
//                 "
//         >
//           <Outlet />
//         </main>
//       </div>
//     </div>
//   );
// }
