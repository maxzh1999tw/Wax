const exerciseFieldSchemasEditDialog = {
    template: `
    <v-dialog v-model="dialog" max-width="500px">
        <v-card>
            <v-card-title>
                <span class="headline">{{ dialogTitle }}</span>
            </v-card-title>
            <v-card-text>
                <v-form ref="form">
                    <v-text-field v-model="editedItem.name" :rules="nameRules" label="欄位名稱" class="mb-3"></v-text-field>
                    <v-text-field v-model="editedItem.unit" :rules="unitRules" maxlength="5" label="單位" class="mb-3"></v-text-field>
                    <v-checkbox v-model="editedItem.isRequired" label="是否為必填欄位" class="mb-3"></v-checkbox>
                    <v-select v-model="editedItem.dataType" :items="dataTypeItems" :rules="dataTypeRules" label="資料類型"></v-select>
                </v-form>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="close">取消</v-btn>
                <v-btn color="blue darken-1" text @click="confirm">確定</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
    `,
    props: {
        dataTypeItems: {
            type: Array,
            required: true,
        },
        nameDuplicateRule: {
            type: Function,
            required: true,
        }
    },
    data() {
        return {
            dialog: false,
            dialogTitle: '',
            editedItem: {
                name: '',
                unit: '',
                isRequired: false,
                dataType: null
            },
            resolve: null,
            reject: null,
            nameRules: [validateRule.lengthRequiredRule, this.nameDuplicateRule],
            unitRules: [validateRule.lengthRequiredRule],
            dataTypeRules: [validateRule.notEmptyRule],
        }
    },
    methods: {
        showForAdd() {
            this.dialogTitle = '新增項目'
            this.dialog = true
            this.editedItem = {
                name: '',
                unit: '',
                isRequired: false,
                dataType: this.dataTypeItems.firstOrDefault()?.value
            }

            this.$nextTick(() => {
                this.$refs.form.$el.querySelector('input').focus();
            });

            return new Promise((resolve, reject) => {
                this.resolve = resolve
                this.reject = reject
            })
        },
        showForEdit(item) {
            this.dialogTitle = '編輯項目'
            this.dialog = true
            this.editedItem = Object.assign({}, item)

            this.$nextTick(() => {
                this.$refs.form.$el.querySelector('input').focus();
            });

            return new Promise((resolve, reject) => {
                this.resolve = resolve
                this.reject = reject
            })
        },
        close() {
            this.dialog = false
            this.resolve(null)
        },
        async confirm() {
            const result = await this.$refs.form.validate()
            if (result.valid) {
                this.dialog = false
                this.resolve(this.editedItem)
            }
        }
    },
}

const exerciseFieldSchemasEditor = {
    components: {
        exerciseFieldSchemasEditDialog
    },
    template: `
    <v-table>
        <thead>
        <tr>
            <th>欄位名稱</th>
            <th>單位</th>
            <th>是否為必填欄位</th>
            <th>資料類型</th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="item in modelValue" :key="item.name">
            <td>{{ item.name }}</td>
            <td>{{ item.unit }}</td>
            <td>
                <v-checkbox v-model="item.isRequired" readonly hide-details></v-checkbox>
            </td>
            <td>{{ getDataTypeTitle(item.dataType) }}</td>
            <td>
            <v-btn @click="openEditDialog(item)" size="small" icon class="mx-1 my-2">
                <v-icon>mdi-pencil</v-icon>
            </v-btn>
            <v-btn @click="deleteItem(item)" size="small" icon class="mx-1 my-2">
                <v-icon>mdi-delete</v-icon>
            </v-btn>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="text-center">
                <v-btn @click="openAddDialog">
                    <v-icon>mdi-plus</v-icon>
                </v-btn>
            </td>
        </tr>
        </tbody>
    </v-table>
    <!-- 新增的錯誤訊息顯示部分 -->
    <div v-if="errors.length">
        <v-divider class="my-2"></v-divider>
        <v-alert type="error">
        <ul>
            <li v-for="error in errors" :key="error">{{ error }}</li>
        </ul>
        </v-alert>
    </div>
    <exerciseFieldSchemasEditDialog ref="editDialog" :dataTypeItems="dataTypeItems" :nameDuplicateRule="fieldNameDuplicateRule"></exerciseFieldSchemasEditDialog>
    `,
    props: {
        modelValue: {
            type: Array,
            required: true,
        },
        dataTypeItems: {
            type: Array,
            required: true,
        },
        rules: {
            type: Array,
            default: [],
        }
    },
    data() {
        return {
            editedIndex: -1,
            errors: []
        }
    },
    methods: {
        validate() {
            this.errors = []
            for (const rule of this.rules) {
                const error = rule(this.modelValue)
                if (error != true) {
                    this.errors.push(error)
                }
            }
            return Promise.resolve({
                valid: this.errors.length == 0,
                errors: this.errors,
            })
        },
        fieldNameDuplicateRule(value) {
            if (this.modelValue.some(item => item.name === value && item !== this.editedItem)) {
                return '此欄位名稱已被使用'
            }
            return true
        },
        async openAddDialog() {
            const newItem = await this.$refs.editDialog.showForAdd()
            if (newItem) {
                this.modelValue.push(newItem)
            }
        },
        async openEditDialog(item) {
            const editedItem = await this.$refs.editDialog.showForEdit(item)
            if (editedItem) {
                Object.assign(item, editedItem)
            }
        },
        async deleteItem(item) {
            const index = this.modelValue.indexOf(item)
            const confirm = await alertHelper.confirm('您確定要刪除這個項目嗎？')
            if (confirm) {
                this.modelValue.splice(index, 1)
            }
        },
        getDataTypeTitle(value) {
            return this.dataTypeItems.find(x => x.value == value).title
        },
    },
    watch: {
        modelValue: {
            handler() {
                this.validate()
            },
            deep: true,
        }
    },
    computed: {
        isValide() {
            return this.validate()
        }
    },
}