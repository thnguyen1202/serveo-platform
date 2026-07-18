import { NavLink } from 'react-router-dom';

import { useAuth } from '@/features/auth/hooks/use-auth';
import { menuItems } from '@/routes/menu-items';

export default function Sidebar() {
  const { permissions } = useAuth();

  return (
    <aside
      className="
w-64
border-r
bg-card
p-4
"
    >
      <h1
        className="
text-xl
font-bold
mb-6
"
      >
        Serveo Admin
      </h1>

      <nav
        className="
space-y-1
"
      >
        {menuItems
          .filter((x) => !x.permission || permissions.includes(x.permission))
          .map((item) => {
            //const Icon = item.icon;

            return (
              <NavLink
                key={item.path}

                to={item.path}

                className={({ isActive }) =>
                  `
flex
items-center
gap-3
rounded-md
px-3
py-2

${isActive ? 'bg-primary text-primary-foreground' : 'hover:bg-muted'}

`
                }
              >
                {/* <Icon size={18} /> */}

                {item.title}
              </NavLink>
            );
          })}
      </nav>
    </aside>
  );
}

// import { NavLink } from "react-router-dom";

// const menus = [
//     {
//         name: "Dashboard",
//         path: "/"
//     },
//     {
//         name: "Products",
//         path: "/products"
//     },
//     {
//         name: "Settings",
//         path: "/settings"
//     }
// ];

// export default function Sidebar() {

//     return (
//         <aside className="
//             w-64
//             bg-gray-900
//             text-white
//             min-h-screen
//             p-4
//         ">

//             <h2 className="
//                 text-xl
//                 font-bold
//                 mb-8
//             ">
//                 Serveo Admin
//             </h2>

//             <nav className="space-y-2">

//                 {
//                     menus.map(item => (

//                         <NavLink
//                             key={item.path}
//                             to={item.path}
//                             className={({isActive}) =>
//                                 `
//                                 block
//                                 px-3
//                                 py-2
//                                 rounded
//                                 ${
//                                     isActive
//                                     ? "bg-blue-600"
//                                     : "hover:bg-gray-700"
//                                 }
//                                 `
//                             }
//                         >
//                             {item.name}
//                         </NavLink>

//                     ))
//                 }

//             </nav>

//         </aside>
//     );
// }
