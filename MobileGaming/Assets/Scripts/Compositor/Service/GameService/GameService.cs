using Addressables;
using Addressables.Components;
using Attributes;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace Service
{
    public class GameService : IGameService
    {
        [DependsOnService] private ISceneService sceneService;
        [DependsOnService] private IInputService inputService;

        private SorcererController sorcererController;
        // TODO - Machine Manager that handles that
        
        private Product currentProduct;
        private IInteractible currentInteractible;

        [ServiceInit]
        public void InitGame()
        {
            LoadAssets();
        }

        private void LoadAssets()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("Cameras", LoadCameras);
        }

        private void LoadCameras(GameObject camerasGo)
        {
            var cameras = Object.Instantiate(camerasGo).GetComponent<CameraComponents>();
            Release(camerasGo);

            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("JoystickCanvas", LoadJoystickCanvas);

            void LoadJoystickCanvas(GameObject joystickCanvasGo)
            {
                var joystickCanvas = Object.Instantiate(joystickCanvasGo).GetComponent<JoystickComponents>();
                Release(joystickCanvasGo);
                
                AddressableHelper.LoadAssetAsyncWithCompletionHandler<GameObject>("SorcererController", LoadSorcererController);

                void LoadSorcererController(GameObject sorcererControllerGo)
                {
                    sorcererController = Object.Instantiate(sorcererControllerGo).GetComponent<SorcererController>();
                    Release(sorcererControllerGo);

                    sorcererController.perspCameraGo = cameras.perspCameraGo;
                    sorcererController.perspCam = cameras.perspCamera;
                    sorcererController.orthoCam = cameras.othoCamera;

                    sorcererController.joystickParentGo = joystickCanvas.parentGo;
                    sorcererController.joystickParentTr = joystickCanvas.parentTr;
                    sorcererController.joystickTr = joystickCanvas.joystickTr;
                    
                    sorcererController.SetVariables();

                    Machine.sorcerer = sorcererController;
                }
            }
        }
    }
}